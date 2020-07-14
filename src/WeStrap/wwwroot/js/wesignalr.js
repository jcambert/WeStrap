'use strict';
(function (window) {
    console.log("Starting WeSignalR JsInterop");
    window.wesignalr = {
        init: function (ref, url) {
            this.ref = ref;
            this.url = url;
            this.connection = new signalR.HubConnectionBuilder()
                .withUrl(url)
                .configureLogging(signalR.LogLevel.Information)
                .build();
            this.connection.on('connected', _ => {
                this.appendMessage("Connected.", "success");
                this.ref.invokeMethodAsync("Connected");
                console.log(this.connection);
            });

            this.connection.on('disconnected', _ => {
                this.appendMessage("Disconnected, invalid token..", "danger");
                this.ref.invokeMethodAsync("Disconnected")
            });

            this.connection.on('operation_pending', (operation) => {
                this.appendMessage('Operation pending.', "light", operation);
            });

            this.connection.on('operation_completed', (operation) => {
                this.appendMessage('Operation completed.', "success", operation);
            });

            this.connection.on('operation_rejected', (operation) => {
                this.appendMessage('Operation rejected.', "danger", operation);
            });

            this.connection.on('signedin', (userId) => {
                this.appendMessage('User connect in.', "success", userId);
            });
            console.log(`WeSignalR url set to ${url}`);
        },
        connect: function (jwt) {
            console.log(jwt);
            if (!jwt || /\s/g.test(jwt)) {
                alert('Invalid JWT.')
                return;
            }
            this.appendMessage("Try Connecting to the Hub...", "secondary");
            this.connection.start()
                .then(() => {
                    this.connection.invoke('initializeAsync', jwt);
                    this.appendMessage("Initialisation", "primary")
                })
                .catch(err => {
                    console.error(err); this.appendMessage("Erreur", "danger", err);
                });
        },
        disconnect() {
            if (this.connection)
                this.connection.stop();
        },
        appendMessage(message, type, data) {
            this.ref.invokeMethodAsync("AppendMessage", message, type, JSON.stringify(data)).then(r => console.log("new Message Append"))
        }
    };

})(window);