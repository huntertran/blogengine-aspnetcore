tinymce.init({
    selector: 'textarea'
});

var vm = new Vue({
    el: "#post-add",
    data: {
        categories: [],
        post: {
            title: "",
            summary: "",
            content: ""
        }
    },
    methods: {
        save: function () {
            tinymce.triggerSave();
            axios.post("/api/Posts/Post", vm.post)
                .then(function (response) {
                    if (response.status === 200) {
                        alert("Post saved");
                    }
                });
        },
        getCategories: function(){
            axios.get("/api/Categories/All").then(function (response) {
                vm.categories = response.data;
            });
        }
    },
    mounted() {
        this.getCategories();
    },
})