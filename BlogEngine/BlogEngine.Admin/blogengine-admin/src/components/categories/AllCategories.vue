<template>
  <v-container>

    <v-data-table
    :headers="headers"
    :items="categories"
    hide-actions
    class="elevation-1">
    <template slot="items" slot-scope="props">
      <td>{{ props.item.id }}</td>
      <td>{{ props.item.name }}</td>
      <td>
        <v-btn small flat color="error" v-on:click="showConfirmation(props.item.id)">Delete</v-btn>
      </td>
    </template>
  </v-data-table>

  <v-dialog
      v-model="showDeleteDialog"
      max-width="290"
    >
      <v-card>
        <v-card-title class="headline">Delete category?</v-card-title>

        <!-- <v-card-text>
          Let Google help apps determine location. This means sending anonymous location data to Google, even when no apps are running.
        </v-card-text> -->

        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn
            color="green darken-1"
            flat="flat"
            @click="categoryIdToDelete = 0;showDeleteDialog = false"
          >
            Cancel
          </v-btn>

          <v-btn
            color="green darken-1"
            flat="flat"
            @click="deleteCategory(categoryIdToDelete)"
          >
            Delete
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

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
      showDeleteDialog: false,
      categoryIdToDelete: 0,
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
    showConfirmation: function(id) {
      var _this = this;
      _this.showDeleteDialog = true;
      _this.categoryIdToDelete = id;
    },
    deleteCategory: function(id) {
      var _this = this;
      axios.delete("/api/categories/delete?id=" + id).then(function(response) {
        if (response.status === 200) {
          _this.message = "Selected category deleted";
          _this.snackbar = true;
          _this.showDeleteDialog = false;
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
