<template>
    <v-form>
      <v-text-field
        v-model="name"
        label="Category Name"
        required></v-text-field>
      <v-btn v-on:click="submit">Submit</v-btn>
    </v-form>
</template>

<script>
import axios from "axios";

export default {
  data: function() {
    return {
      name: ""
    };
  },
  methods: {
    submit: function() {
      var category = {
        name: this.name
      };
      var _this = this;
      axios.post("/categories/post", category).then(function(response) {
        if (response.status === 201) {
          _this.$router.push("/categories");
        }
      });
    }
  }
};
</script>
