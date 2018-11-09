import Vue from "vue";
import Vuetify from "vuetify";
import Axios from "axios";

import router from "./router";
import store from "./store";

import "./registerServiceWorker";
import "./plugins/vuetify";
import "vuetify/dist/vuetify.min.css";
import "material-design-icons-iconfont/dist/material-design-icons.css";

import App from "./App.vue";

Vue.use(Vuetify);

Vue.prototype.$http = Axios;
Vue.config.productionTip = false;

const token = localStorage.getItem("token");

// Vue.prototype.$http.defaults.headers.common["Access-Control-Allow-Origin"] =
//   "*";

if (token) {
  Vue.prototype.$http.defaults.headers.common["Authorization"] = token;
}

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount("#app");
