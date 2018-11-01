var vm = new Vue({
    el: "#user-table",
    data: {
        results: []
    },
    methods: {
        getAll: function () {
            axios.get("/api/Users/All").then(function (response) {
                vm.results = response.data;
            });
        },
        showEditModal: function (id) {
            axios.get("/api/Users/Find?userId=" + id)
                .then(function (response) {
                    editVm.user = response.data;
                });

            $("#user-edit").modal("show");
        }
    },
    mounted() {
        this.getAll();
    }
});

var editVm = new Vue({
    el: "#user-edit",
    data: {
        user: {
            id: "",
            userName: "",
            email: "",
            phoneNumber: "",
            roles: []
        }
    },
    methods: {
        edit: function () {
            axios.put("/api/Users/Put", editVm.user)
                .then(function (response) {
                    if (response.status === 200) {
                        // update category in vm
                        vm.getAll();
                        $("#user-edit").modal("hide");
                    }
                });
        }
    }
});