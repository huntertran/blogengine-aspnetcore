<template>
<v-container fluit>
    <v-layout column>
        <v-flex xs6>
            <v-form>
                <v-text-field 
                    v-model="Username"
                    type="email"
                    placeholder="Username"
                    label="Email"></v-text-field>
                <v-text-field 
                    v-model="Password"
                    type="password"
                    placeholder="Password"
                    label="Password"></v-text-field>
                <v-btn v-on:click="login">Login</v-btn>
            </v-form>
        </v-flex>
    </v-layout>
</v-container>
</template>

<script>
export default {
  data() {
    return {
      Username: "",
      Password: ""
    };
  },
  methods: {
    login: function() {
      let Username = this.Username;
      let Password = this.Password;
      this.$store
        .dispatch("login", { Username, Password })
        .then(() => this.$router.push("/"))
        .catch(err => console.log(err));
    }
  }
};
</script>
