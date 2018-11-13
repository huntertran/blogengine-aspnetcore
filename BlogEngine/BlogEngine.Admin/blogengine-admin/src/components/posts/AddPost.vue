<template>
    <v-form>
        <v-text-field
            v-model="post.title"
            label="Post Title"
            required></v-text-field>

        <v-text-field
            v-model="post.summary"
            label="Post Summary"
            required></v-text-field>
        <v-text-field
            v-model="post.content"
            label="Post Content"
            required></v-text-field>
      <v-btn v-on:click="submit">Submit</v-btn>
    </v-form>
</template>

<script>
import axios from "axios";

export default {
  data: function() {
    return {
      post: {
        title: "",
        summary: "",
        content: ""
      }
    };
  },
  methods: {
    submit: function() {
      var _this = this;
      axios.post("/posts/post", _this.post).then(function(response) {
        if (response.status === 200 || response.status === 201) {
          _this.$router.push("/posts");
        }
      });
    }
  }
};
</script>
