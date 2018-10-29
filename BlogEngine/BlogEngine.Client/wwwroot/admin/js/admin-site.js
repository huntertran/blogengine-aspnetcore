var authorizedAxios = axios.create({
    headers: {
        'Authorization': 'Bearer ' + localStorage.token
    }
});

var admin = {
    init: function() {
        admin.assignMenuToggleButton();
    },

    assignMenuToggleButton: function() {
        $("#menu-toggle").on("click",
            function(e) {
                e.preventDefault();
                $("#wrapper").toggleClass("toggled");
            });
    }
};

$(document).ready(function() {
    admin.init();
});