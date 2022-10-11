import axios from "axios";

export class Api {
  constructor() {
    this.client = axios.create();
    this.client.defaults.baseURL = "http://localhost:80";
    this.client.defaults.headers["Access-Control-Allow-Origin"] = "*";
    this.client.defaults.headers["Content-Type"] =
      "application/json;charset=UTF-8";
    this.client.defaults.withCredentials = true;
    this.client.timeout = 3000;
  }

  clientWrapper = (method, url, data, config = {}) => {
    const clientResult = this.client[method](url, data, config);
    return clientResult;
  };

  getUser = (id) => this.clientWrapper("get", `api/auth?id=${id}`);
  
}
const api = new Api();

export default api;
