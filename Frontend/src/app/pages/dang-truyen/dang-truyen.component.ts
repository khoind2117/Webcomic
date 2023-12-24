import { Component } from '@angular/core';

@Component({
  selector: 'app-dang-truyen',
  templateUrl: './dang-truyen.component.html',
  styleUrls: ['./dang-truyen.component.css']
})
export class DangTruyenComponent {
  showScrollToTopButton = false;


  showSaveSuccessAlert() {
    alert('Đã lưu truyện thành công');
  }


  scrollToTop() {
    window.scrollTo({ top: 0, behavior: 'smooth' });
  }
  changeBackgroundColor() {
    const colorSelect = document.getElementById("colorSelect") as HTMLSelectElement;
    const selectedColor = colorSelect.value;

    // Thay đổi màu nền của body
    document.body.style.backgroundColor = selectedColor;

    // Kiểm tra màu nền và đặt màu văn bản
    if (selectedColor === 'black') {
      document.body.style.color = 'white'; // Đặt màu văn bản thành trắng nếu nền là đen
    } else {
      document.body.style.color = 'black'; // Đặt màu văn bản mặc định nếu nền không phải là đen
    }
  }
  isMenuVisible = false;

  showUserMenu() {
    this.isMenuVisible = true;
  }

  hideUserMenu() {
    this.isMenuVisible = false;
  }
}
