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
        <v-btn small flat color="success" v-on:click="showEditCategoryDialog(props.item.id)">Edit</v-btn>
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

  <v-dialog v-model="edit.show" max-width="290">
    <v-card>
      <v-card-title class="headline">Edit Category</v-card-title>

      <v-card-text>
        <v-container grid-list-md>
          <v-layout wrap>
            <v-flex xs12>
                <v-text-field v-model="edit.category.name" label="Category Name" required></v-text-field>
              </v-flex>
          </v-layout>
        </v-container>
      </v-card-text>

      <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn
            color="green darken-1"
            flat="flat"
            @click="edit.show = false"
          >
            Cancel
          </v-btn>

          <v-btn
            color="green darken-1"
            flat="flat"
            @click="editCategory(edit.category)"
          >
            Save
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
      edit: {
        show: false,
        category: {
          id: 0,
          name: ""
        }
      },
      headers: [
        { text: "No.", align: "left", value: "id" },
        { text: "Name", align: "left", value: "name" },
        { text: "Action", align: "left", value: "action", sortable: false }
      ],
      categories: []
    };
  },
  methods: {
    getSingleCategoryFromArray: function(id) {
      var _this = this;
      var filteredItems = _this.categories.filter(function(element) {
        return element.id === id;
      });

      return filteredItems[0].name;
    },
    getAllCategories: function() {
      var _this = this;
      axios.get("/categories/all").then(function(response) {
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
      axios.delete("/categories/delete?id=" + id).then(function(response) {
        if (response.status === 200) {
          _this.message = "Selected category deleted";
          _this.snackbar = true;
          _this.showDeleteDialog = false;
          _this.getAllCategories();
        }
      });
    },
    showEditCategoryDialog: function(id) {
      var _this = this;
      _this.edit.category.id = id;
      _this.edit.category.name = _this.getSingleCategoryFromArray(id);
      _this.edit.show = true;
    },
    editCategory: function(category) {
      var _this = this;
      axios.put("/categories/put", category).then(function(response) {
        if (response.status === 200) {
          _this.edit.show = false;
          _this.getAllCategories();
        }
      });
    }
  },
  mounted() {
    this.getAllCategories();
  }
};
</script>
