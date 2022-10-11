import React from "react";
import WordsStore from "./wordsStore";

class Store {
  constructor() {
    this.wordsStore = new WordsStore(this);
  }
}

export const storeContext = React.createContext(new Store());
export const useStore = () => React.useContext(storeContext);
