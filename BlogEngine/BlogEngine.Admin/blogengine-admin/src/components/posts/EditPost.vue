<template>
  <v-container>
    <v-layout>
      <v-flex xs12>
        <v-form>
          <v-text-field
            v-model="post.title"
            label="Post Title"
            required
          ></v-text-field>

          <v-text-field
            v-model="post.summary"
            label="Post Summary"
            required
          ></v-text-field>
          <v-text-field
            v-model="post.content"
            label="Post Content"
            required
          ></v-text-field>
          <v-btn v-on:click="submit">Submit</v-btn>
        </v-form>
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script>
export default {
    
}
</script>
