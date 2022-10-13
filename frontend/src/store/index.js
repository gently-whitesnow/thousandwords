import React from "react";
import GlobalStore from "./globalStore";
import WordsStore from "./wordsStore";

class Store {
  constructor() {
    this.wordsStore = new WordsStore(this);
    this.globalStore = new GlobalStore(this);
  }
}

export const storeContext = React.createContext(new Store());
export const useStore = () => React.useContext(storeContext);
