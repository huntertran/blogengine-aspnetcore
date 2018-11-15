<template>
    <v-container>
        <!-- <v-layout text-xs-center wrap>
            <v-flex xs6>
                <h1>Total Post: {{totalPost}}</h1>
            </v-flex>
            <v-flex xs6>
                <h1>Total Category: {{totalCategory}}</h1>
            </v-flex>
        </v-layout> -->
        <router-view></router-view>
    </v-container>
</template>

<script>
export default {
  data: function() {
    return {
      totalPost: 5,
      totalCategory: 5
    };
  },
  computed: {
    isLoggedIn: function() {
      return this.$store.getters.isLoggedIn;
    }
  },
  mounted() {
    if (!this.isLoggedIn) {
      this.$router.push("/admin/login");
    }
  }
};
</script>
