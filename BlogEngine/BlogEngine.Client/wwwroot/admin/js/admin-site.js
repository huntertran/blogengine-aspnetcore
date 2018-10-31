var authorizedAxios = axios.create({
    headers: {
        'Authorization': 'Bearer ' + localStorage.token
    }
});

axios.interceptors.request.use(function (config) {
    config.headers['Authorization'] = 'Bearer ' + localStorage.token;
    return config;
}, function (err) {
    return Promise.reject(err);
});

var admin = {
    init: function () {
        admin.assignMenuToggleButton();
    },

    assignMenuToggleButton: function () {
        $("#menu-toggle").on("click",
            function (e) {
                e.preventDefault();
                $("#wrapper").toggleClass("toggled");
            });
    }
};

$(document).ready(function () {
    admin.init();
});