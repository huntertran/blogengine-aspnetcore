var vm = new Vue({
    el: "#post-table",
    data: {
        results: []
    },
    methods: {
        getAll: function () {
            axios.get("/api/Posts/All").then(function (response) {
                vm.results = response.data;
            });
        }
    },
    mounted() {
        this.getAll();
    }
});