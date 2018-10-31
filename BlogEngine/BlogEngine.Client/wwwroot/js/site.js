var login = {
    init: function () {
        login.getTokenOnLogin();
    },

    getTokenOnLogin: function () {
        $(".login-form").on("submit",
            function (e) {
                var username = $("#Input_Email").val();
                var password = $("#Input_Password").val();
                axios.get("/api/auth/login?Username=" + username + "&Password=" + password)
                    .then(function(response){
                        localStorage.token = response.data.AccessToken;
                        localStorage.expiredIn = response.data.ExpiredIn;
                    });
                // $.ajax({
                //         url: "/api/auth/login?Username=" + username + "&Password=" + password
                //     })
                //     .done(function (data) {
                //         localStorage.token = data.AccessToken;
                //         localStorage.expiredIn = data.ExpiredIn;
                //     });
            });
    }
};

$(document).ready(function () {
    login.init();
});