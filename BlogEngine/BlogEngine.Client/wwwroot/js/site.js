// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


var login = {
    axios: function() {
        return window.require("axios");
    },

    init: function() {
        login.getTokenOnLogin();
    },

    configAxios: function() {
        axios.defaults.baseUrl = "/api/";
    },

    getTokenOnLogin: function() {
        $(".login-form").on("submit",
            function (e) {
                var username = $("#Input_Email").val();
                var password = $("#Input_Password").val();
                $.ajax({
                        url: "/api/auth/login?Username=" + username + "&Password=" + password
                    })
                    .done(function(data) {
                        localStorage.token = data.AccessToken;
                        localStorage.expiredIn = data.ExpiredIn;
                    });
            });
    }
};

$(document).ready(function() {
    login.init();
});