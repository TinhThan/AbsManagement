import ReactDOM from "react-dom";

export const renderModal = (modal) => {
  const _modalContainer = document.getElementById('draw-container')
  console.log("_modalContainer",_modalContainer)
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