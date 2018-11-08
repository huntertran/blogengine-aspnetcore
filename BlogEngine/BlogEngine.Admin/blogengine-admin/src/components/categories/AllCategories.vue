<template>
  <v-data-table
    :headers="headers"
    :items="categories"
    hide-actions
    class="elevation-1"
  >
    <template slot="items" slot-scope="props">
      <td>{{ props.item.no }}</td>
      <td class="text-xs-right">{{ props.item.name }}</td>
      <td class="text-xs-right">{{ props.item.action }}</td>
    </template>
  </v-data-table>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      headers: [
        { text: "No.", align: "left", sortable: false, value: "no" },
        { text: "Name", align: "left", value: "name" },
        { text: "Action", align: "left", value: "action" }
      ],
      categories: []
    };
  },
  methods: {
    getAllCategories: function() {
      axios.get("/api/Categories/All").then(function(response) {
        this.categories = response.data;
      });
    },
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
