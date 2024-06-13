import Axios from "axios";

export default defineNuxtPlugin(() => {
	Axios.defaults.baseURL = "https://localhost:7061";
});
