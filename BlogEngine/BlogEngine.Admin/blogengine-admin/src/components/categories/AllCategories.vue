<template>
  <v-container>
    <v-data-table
    :headers="headers"
    :items="categories"
    hide-actions
    class="elevation-1"
    >
    <template slot="items" slot-scope="props">
      <td>{{ props.item.id }}</td>
      <td>{{ props.item.name }}</td>
      <td>
        <v-btn small flat color="error" v-on:click="deleteCategory(props.item.id)">Delete</v-btn>
      </td>
    </template>
  </v-data-table>
  <v-snackbar v-model="snackbar" :right="true" :bottom="true" :timeout="500">
      {{ message }}
      <v-btn
        color="pink"
        flat
        @click="snackbar = false"
      >
        Close
      </v-btn>
    </v-snackbar>
  </v-container>
  
</template>

<script>
import axios from "axios";

export default {
  data: function() {
    return {
      snackbar: false,
      message: "",
      headers: [
        { text: "No.", align: "left", value: "id" },
        { text: "Name", align: "left", value: "name" },
        { text: "Action", align: "left", value: "action", sortable: false }
      ],
      categories: []
    };
  },
  methods: {
    getAllCategories: function() {
      var _this = this;
      axios.get("/api/Categories/All").then(function(response) {
        _this.categories = response.data;
      });
    },
    deleteCategory: function(id) {
      var _this = this;
      // Show confirm

      axios.delete("/api/categories/delete?id=" + id).then(function(response) {
        if (response.status === 200) {
          _this.message = "Selected category deleted";
          _this.snackbar = true;
          _this.getAllCategories();
        }
      });
    }
    // showEditModal: function(id) {
    //   axios.get("/api/Categories/Find?id=" + id).then(function(response) {
    //     editCategory.category = response.data;
    //   });

    //   $("#category-edit").modal("show");
    // },
    // addCategory: function() {
    //   addCategory.category.name = "";
    //   $("#category-add").modal("show");
    // },
    // deleteCategory: function(category) {
    //   var result = confirm(
    //     "Do you want to delete category '" + category.name + "' pernamently?"
    //   );
    //   if (result) {
    //     axios
    //       .delete("/api/Categories/Delete?id=" + category.id)
    //       .then(function(response) {
    //         if (response.status === 200) {
    //           alert("Category '" + category.name + "' is deleted");
    //           vm.getAllCategories();
    //         }
    //       });
    //   }
    // }
  },
  mounted() {
    this.getAllCategories();
  }
};
</script>
