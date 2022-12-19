import { createApp } from "vue";
import App from "./App.vue";
import mitt from "mitt";
import VueClickAway from "vue3-click-away";


// Icon css
import "./css/icon.css";
import "./css/tooltip.css";
const eventBus = mitt();
const app = createApp(App);
app.use(VueClickAway);

app.config.globalProperties.eventBus = eventBus;
app.mount("#app");