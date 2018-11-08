import Vue from "vue";
import VueRouter from "vue-router";

import About from "./components/About";
import HelloWorld from "./components/HelloWorld";

Vue.use(VueRouter);

const router = new VueRouter({
  mode: "history",
  routes: [
    {
      path: "/",
      name: "home",
      component: HelloWorld
    },
    {
      path: "/About",
      name: "about",
      component: About
    }
  ]
});

export default router;
