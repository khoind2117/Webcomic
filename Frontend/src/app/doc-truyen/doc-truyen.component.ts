import { Component } from '@angular/core';

@Component({
  selector: 'app-doc-truyen',
  templateUrl: './doc-truyen.component.html',
  styleUrls: ['./doc-truyen.component.css']
})
export class DocTruyenComponent {
  showConsoleMessage(): void {
    console.log("Nút đã được nhấn!");
  }

  changeBackgroundColor(): void {
    var color = (document.getElementById("colorSelect") as HTMLSelectElement).value;
    var colorbg = (document.getElementsByClassName("story-container")[0] as HTMLDivElement);
    colorbg.style.backgroundColor = color;
  }

  toggleDropdown(): void {
    var dropdown = (document.getElementById("dropdown") as HTMLDivElement);
    if (dropdown.style.display === "none" || dropdown.style.display === "") {
      dropdown.style.display = "block";

      // Đặt overlay khi form popup mở
      var overlay = (document.getElementById("overlay") as HTMLDivElement);
      overlay.style.display = "block";
    } else {
      dropdown.style.display = "none";

      // Ẩn overlay khi form popup đóng
      var overlay = (document.getElementById("overlay") as HTMLDivElement);
      overlay.style.display = "none";
    }
  }

  comment: String[] = [];

  abc(): void {
    let a = (document.getElementById('chat-input') as HTMLInputElement).value;
    this.comment.push(a);
  }

  changeFont() {
    const fontSelect = document.getElementById("fontSelect") as HTMLSelectElement;
    const selectedFont = fontSelect.value;

    const nộiDung = (document.getElementsByClassName("story-container")[0] as HTMLDivElement);
    nộiDung.style.fontFamily = selectedFont;
  }

  changeFontSize() {
    const fontSizeInput = document.getElementById("fontSize") as HTMLInputElement;
    const fontSize = fontSizeInput.value;

    const nộiDung = (document.getElementsByClassName("story-container")[0] as HTMLDivElement);
    nộiDung.style.fontSize = fontSize + "px";
  }
  changeDistance() {
    const DistanceInput = document.getElementById("distance") as HTMLInputElement;
    const margin = DistanceInput.value;

    const nộiDung = (document.getElementsByClassName("story-container")[0] as HTMLDivElement);
    nộiDung.style.margin = margin + "px";
  }
  closePopup(): void {
    var dropdown = (document.getElementById("dropdown") as HTMLDivElement);
    dropdown.style.display = "none";

    // Ẩn overlay khi form popup đóng
    var overlay = (document.getElementById("overlay") as HTMLDivElement);
    overlay.style.display = "none";
  }
}
