import ReactDOM from "react-dom";

export let rootModal: any;
export const renderModal = (modal: JSX.Element) => {
  const _modalContainer = document.getElementById('modal-container')
  if (!_modalContainer) {
    return;
  }
  ReactDOM.render(modal, _modalContainer)
  return {
    render() {
      console.log(1);
      
    },
    unmount() {
      if (!_modalContainer) {
        return;
      }
      ReactDOM.unmountComponentAtNode(_modalContainer)
    },
  };
};