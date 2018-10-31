var vm = new Vue({
    el: "#category",
    data: {
        results: []
    },
    methods: {
        getAllCategories: function () {
            axios.get("/api/Categories/All").then(function (response) {
                vm.results = response.data;
            });
        },
        showEditModal: function (id) {
            axios.get("/api/Categories/Find?id=" + id)
                .then(function (response) {
                    editCategory.category = response.data;
                });

            $("#category-edit").modal("show");
        },
        addCategory: function () {
            $("#category-add").modal("show");
        },
        deleteCategory: function (category) {
            var result = confirm("Do you want to delete category '" + category.name + "' pernamently?");
            if (result) {
                axios.delete("/api/Categories/Delete?id=" + category.id)
                    .then(function (response) {
                        if (response.status === 200) {
                            alert("Category '" + category.name + "' is deleted");
                            vm.getAllCategories();
                        }
                    });
            }
        }
    },
    mounted() {
        this.getAllCategories();
    }
});

var editCategory = new Vue({
    el: "#category-edit",
    data: {
        category: {
            id: "",
            name: ""
        }
    },
    methods: {
        editCategory: function () {
            axios.put("/api/Categories/Put", editCategory.category)
                .then(function (response) {
                    if (response.status === 200) {
                        // update category in vm
                        vm.getAllCategories();
                        $("#category-edit").modal("hide");
                    }
                });
        }
    }
});

var addCategory = new Vue({
    el: "#category-add",
    data: {
        category: {
            name: ""
        }
    },
    methods: {
        addCategory: function () {
            axios.post("/api/Categories/Post", addCategory.category)
                .then(function (response) {
                    if (response.status === 201) {
                        // update category in vm
                        vm.getAllCategories();
                        $("#category-add").modal("hide");
                    }
                });
        }
    }
});