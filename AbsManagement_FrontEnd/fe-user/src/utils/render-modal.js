import ReactDOM from "react-dom";

export const renderModal = (modal) => {
  const _modalContainer = document.getElementById('modal-container')
  if (!_modalContainer) {
    return;
  }
  ReactDOM.render(modal, _modalContainer)
  return {
    render() {
    },
    unmount() {
      if (!_modalContainer) {
        return;
      }
      ReactDOM.unmountComponentAtNode(_modalContainer)
    },
  };
};