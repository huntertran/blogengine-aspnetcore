import Vue from 'vue';
import Vuetify from 'vuetify';
import Axios from 'axios';

import router from './router';
import store from './store';

// import "./registerServiceWorker";
import './plugins/vuetify';
import 'vuetify/dist/vuetify.min.css';
import 'material-design-icons-iconfont/dist/material-design-icons.css';

import App from './App.vue';

Vue.use(Vuetify);

Vue.prototype.$http = Axios;
Vue.config.productionTip = false;

const token = localStorage.getItem('token');

// Set Axios base URL
// Vue.prototype.$http.defaults.baseURL = 'https://localhost:44394/api';
Vue.prototype.$http.defaults.baseURL = '/api';
// Vue.prototype.$http.defaults.baseURL =
//   'https://hunter-blogengine.azurewebsites.net/api';

// Set CORS
Vue.prototype.$http.defaults.crossDomain = true;

if (token) {
  Vue.prototype.$http.defaults.headers.common['Authorization'] =
    'Bearer ' + token;
}

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app');
