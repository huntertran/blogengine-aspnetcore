<template>
    <v-layout text-xs-center wrap>
        <v-flex xs6>
            <h1>Total Post: {{totalPost}}</h1>
        </v-flex>
        <v-flex xs6>
            <h1>Total Category: {{totalCategory}}</h1>
        </v-flex>
    </v-layout>
</template>

<script>
import axios from 'axios';

export default {
  data: function() {
    return {
      totalPost: 0,
      totalCategory: 0
    };
  },
  methods: {
    getTotalPost: function() {
      var _this = this;
      axios.get('/posts/GetTotalPostNumber').then(function(response) {
        _this.totalPost = response.data;
      });
    },
    getTotalCategory: function() {
      var _this = this;
      axios.get('/categories/GetTotalCategoryNumber').then(function(response) {
        _this.totalCategory = response.data;
      });
    }
  },
  mounted: function() {
    this.getTotalPost();
    this.getTotalCategory();
  }
};
</script>
