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
    
 <!-- <div>
   <form class="login" @submit.prevent="login">
     <h1>Sign in</h1>
     <label>Email</label>
     <input required v-model="email" type="email" placeholder="Name"/>
     <label>Password</label>
     <input required v-model="password" type="password" placeholder="Password"/>
     <hr/>
     <button type="submit">Login</button>
   </form>
 </div> -->
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
