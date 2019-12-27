<template>
  <router-view></router-view>
</template>

<script>
export default {
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
