import { makeAutoObservable, configure } from "mobx";

class WordsStore {
  constructor(rootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);
    configure({
      enforceActions: "never",
    });
  }

  
}

export default WordsStore;
