import 'bootstrap/dist/css/bootstrap.css';
import 'react-toastify/dist/ReactToastify.min.css'
import './Assets/css/app.css'
import './Assets/css/NavMenu.css'
import './Assets/css/loader.css'
import './Assets/css/custom.css'
import * as ReactDOM from 'react-dom';
import App from './App';
import { toast } from 'react-toastify';
import { Provider } from 'react-redux';
import store from './ReduxStore/store';

// Get the application-wide store instance, prepopulating with state from the server where available.
toast.configure({ theme: 'colored', autoClose: 5000, position: toast.POSITION.TOP_RIGHT })

ReactDOM.render(
    <Provider store={store}>
        <App />
    </Provider>,
    document.getElementById('root'));

