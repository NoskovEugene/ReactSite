import RootStore from "../Stores/RootStore";
import App from "../App";
import {createBrowserHistory, History} from 'history';
import SessionStorageController, {Constants} from "../Controllers/SessionStorageController";

export const ApplicationStartup = async () =>{

    SessionStorageController.clear();
    SessionStorageController.setItem("https://localhost:5001", Constants.backendUrl);

    const rootStore = await RootStore.build();
    const history: History = createBrowserHistory();


    const app = <App rootStore={rootStore} history={history}/>;

    return {rootStore, app};
}