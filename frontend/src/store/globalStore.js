import { makeAutoObservable, configure } from "mobx";
import api from "../api";
import mockWords from "../mocks/words.json";

class GlobalStore {
  constructor(rootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);
    configure({
      enforceActions: "never",
    });
  }
  auth = true;
  email = null;
  validEmail = false;

  setEmail = (result) => {
    this.email = result;
  };
  setAuth = (result) => {
    this.auth = result;
  };
  setValidEmail = (result) => {
    this.validEmail = result;
  };
}

export default GlobalStore;
