tinymce.init({
    selector: 'textarea',
    setup: function(editor){
        editor.on('keyup', function(){
            var new_value = tinymce.get('editor').getContent(self.value);
        })
    }
});

var vm = new Vue({
    el: "#post-add",
    data: {
        categories: [],
        post: {
            title: "",
            summary: "",
            content: "",
            postCategories: []
        }
    },
    methods: {
        save: function (event) {
            event.preventDefault();
            tinymce.triggerSave();


            //get selected categories
            vm.categories.forEach(element => {
                if(element.isSelected){
                    var postCategory = {
                        categoryId = element.id
                    }

                    vm.categories.push(postCategory);
                }
            });

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