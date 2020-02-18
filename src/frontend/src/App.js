import React from 'react';
import logo from './logo.svg';
import axios from 'axios';
import './App.css';

export default class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      id: 'get-id-from-somewhere',
      file: null,
    };
  }

  async submit(e) {
    e.preventDefault();

    const url = `https://localhost:5001/api/upload`;
    const formData = new FormData();
    formData.append('arquivo', this.state.file);
    const config = {
      headers: {
        'content-type': 'multipart/form-data',
      },
    };
    axios.post(url, formData, config).then((resp) => {
      console.log('passou');
    }).catch((error) => {
      console.log('erro', error);
    });
  }

  setFile(e) {
    this.setState({ file: e.target.files[0] });
  }
  render(){
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
          <p>
            Informe o arquivo xlsx abaixo.
          </p>
          <form onSubmit={e => this.submit(e)}>
            <input type="file" onChange={e => this.setFile(e)} />
            <button type="submit">Enviar</button>
          </form>

        </header>
      </div>
    );
  }
}
