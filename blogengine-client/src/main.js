import Vue from 'vue';
import vuetify from './plugins/vuetify';

import Axios from 'axios';

import router from './router';
import store from './store';

import './plugins/vuetify';

import App from './App.vue';

Vue.prototype.$http = Axios;
Vue.config.productionTip = false;

const token = localStorage.getItem('token');

// Set Axios base URL
Vue.prototype.$http.defaults.baseURL = 'https://localhost:5001/api';
// Vue.prototype.$http.defaults.baseURL = '/api';
// Vue.prototype.$http.defaults.baseURL =
//   'https://hunter-blogengine.azurewebsites.net/api';

// Set CORS
Vue.prototype.$http.defaults.crossDomain = true;

if (token) {
  Vue.prototype.$http.defaults.headers.common['Authorization'] =
    'Bearer ' + token;
}

new Vue({
  vuetify,
  router,
  store,
  render: h => h(App)
}).$mount('#app');
