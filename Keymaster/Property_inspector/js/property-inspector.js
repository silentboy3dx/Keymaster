// global websocket, used to communicate from/to Stream Deck software
// as well as some info about our plugin, as sent by Stream Deck software 
var websocket = null,
    uuid = null,
    inInfo = null,
    actionInfo = {},
    settingsModel = {
        Counter: 0,
        Text: '',
    };

function connectElgatoStreamDeckSocket(inPort, inUUID, inRegisterEvent, inInfo, inActionInfo) {
    uuid = inUUID;
    actionInfo = JSON.parse(inActionInfo);
    inInfo = JSON.parse(inInfo);
    websocket = new WebSocket('ws://localhost:' + inPort);

    if (typeof actionInfo.payload.settings.settingsModel !== "undefined") {
        restoreSettings(actionInfo.payload.settings.settingsModel)
    }

    websocket.onopen = function () {
        var json = {event: inRegisterEvent, uuid: inUUID};
        // register property inspector to Stream Deck
        websocket.send(JSON.stringify(json));
    };

    websocket.onmessage = function (evt) {
        // Received message from Stream Deck
        var jsonObj = JSON.parse(evt.data);
        var sdEvent = jsonObj['event'];
        switch (sdEvent) {
            case "didReceiveSettings":
                restoreSettings(settingsModel);
                break;
            default:
                break;
        }
    };
}


const setSettings = (settingsModel) => {
    if (websocket) {
        let json = {
            "event": "setSettings",
            "context": uuid,
            "payload": {
                "settingsModel": settingsModel
            }
        };
        websocket.send(JSON.stringify(json));
    }
};



const restoreSettings = (settingsModel) => {
    Object.keys(settingsModel).forEach((key) => {
        let elm = document.querySelector(`[data-name="${key}"]`)
        if (elm && typeof elm !== 'undefined') {
            if (elm.type === 'file') {
                const info = document.querySelector(`[data-fileinfo="${key}"]`);
                if (info) {
                    const s = settingsModel[key].split('/').pop();
                    info.textContent = s.length > 28
                        ? s.substr(0, 10)
                        + '...'
                        + s.substr(s.length - 10, s.length)
                        : s;
                }
            } else {
                elm.value = settingsModel[key]
            }
        }
    });
}

const saveSettings = () => {

    let settings = {};
    let properties = document.querySelectorAll(".property_value");

    Object.values(properties).forEach((property) => {
        if (typeof property.dataset.name != "undefined") {
            let key = property.dataset.name;
            
            if (property.type === 'file') {
                if (settings[key] !== property.value) {
                    settings[key] = decodeURIComponent(property.value.replace(/^C:\\fakepath\\/, ''))
                }
            }  else {
                settings[key] = property.value
            }
        }
    });

    if (Object.keys(settings).length > 0) {
        setSettings(settings);
    }
}

const updateFileInfo = (e) => {
    
    const target = e.target
   
    const info = document.querySelector(`[data-fileinfo="${target.dataset.name}"]`);
    if (info) {
        const s = target.value.split('/').pop();
        info.textContent = s.length > 28
            ? s.substr(0, 10)
            + '...'
            + s.substr(s.length - 10, s.length)
            : s;
    }
}

document.addEventListener('DOMContentLoaded', () => {
    let properties = document.querySelectorAll(".property_value");
    Object.values(properties).forEach((property) => {
        if (property.value.length === 0) {
            if (typeof property.dataset.default != "undefined") {
                property.value = property.dataset.default;
            }
        }

        
        if (property.type === 'file') {
           property.addEventListener('change', updateFileInfo)
        }

        property.addEventListener("change", saveSettings)
    });
});





