import { makeAutoObservable, configure } from "mobx";
import api from "../api";


class GlobalStore {
  constructor(rootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);
    configure({
      enforceActions: "never",
    });
  }
  auth = false;
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

  sendEmailhandler = (email) => {
    api
      .postEmail(email)
      .then(() => {
        this.setAuth(true);
      })
      .catch((err) => {
        this.setAuth(false);
        console.error(err);
      });
  };
  authHandler = () => {
    api
      .getAuth()
      .then(() => {
        this.setAuth(true);
      })
      .catch((err) => {
        this.setAuth(false);
        console.error(err);
      });
  };
}

export default GlobalStore;
