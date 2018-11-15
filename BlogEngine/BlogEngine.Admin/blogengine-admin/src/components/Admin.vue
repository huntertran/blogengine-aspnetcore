<template>
  <router-view></router-view>
    <!-- <v-container fluid grid-list-md>
        
    </v-container> -->
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
