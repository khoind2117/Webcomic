// chinh-sua-truyen.component.ts

import { Component, OnInit, Renderer2, ElementRef } from '@angular/core';
import { AngularEditorConfig } from '@kolkov/angular-editor';
@Component({
  selector: 'app-them-truyen-moi',
  templateUrl: './them-truyen-moi.component.html',
  styleUrls: ['./them-truyen-moi.component.css']
})
export class ThemTruyenMoiComponent implements OnInit {

  constructor(private renderer: Renderer2, private el: ElementRef) { }
  config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '15rem',
    minHeight: '5rem',
    placeholder: 'Enter text here...',
    translate: 'no',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Arial',
  };
  ngOnInit() {
    // Khởi tạo trạng thái ban đầu nếu cần
    this.switchTab('QuanLyThongTinTruyen');
    this.ngayDang = this.formatDate(new Date());
  }
  
  switchTab(tabName: string) {
    let tabContents: NodeListOf<Element> = document.querySelectorAll(".tabcontent");
    tabContents.forEach(element => {
      this.renderer.setStyle(element, 'display', 'none');
    });

    let tabLinks: NodeListOf<Element> = document.querySelectorAll(".tablinks");
    tabLinks.forEach(element => {
      element.classList.remove("active");
    });

    let selectedTab = document.getElementById(tabName);
    if (selectedTab) {
      this.renderer.setStyle(selectedTab, 'display', 'block');
    }

    let currentTabLink = this.el.nativeElement.querySelector(`[onclick="switchTab('${tabName}')"`);
    if (currentTabLink) {
      currentTabLink.classList.add("active");
    }
  }
  previewUrl: string | ArrayBuffer = ''; // Khởi tạo giá trị mặc định là chuỗi rỗng
  previewImage(event: any) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.previewUrl = e.target.result;
      };
      reader.readAsDataURL(file);
    } else {
      this.previewUrl = ''; // Gán giá trị mặc định khi không có ảnh
    }
  }
  ngayDang!: string; // Định dạng YYYY-MM-DD



  // Hàm chuyển đổi đối tượng ngày thành chuỗi định dạng YYYY-MM-DD
  private formatDate(date: Date): string {
    const year = date.getFullYear();
    const month = ('0' + (date.getMonth() + 1)).slice(-2);
    const day = ('0' + date.getDate()).slice(-2);
    return `${year}-${month}-${day}`;
  }

  validateAndSwitchTab(nextTabName: string) {
    // Kiểm tra các trường dữ liệu ở tab hiện tại
    if (!this.validateCurrentTab()) {
      alert('Vui lòng điền đầy đủ thông tin trước khi chuyển tab!');
      return;
    }

    // Nếu thông tin hợp lệ, thì chuyển tab
    this.switchTab(nextTabName);
  }

  validateCurrentTab(): boolean {
    // Kiểm tra các trường dữ liệu ở tab hiện tại
    const tenTruyen = document.getElementById('tenTruyen') as HTMLInputElement;
    const tenTacGia = document.getElementById('tenTacGia') as HTMLInputElement;
    const danhMuc = document.getElementById('danhMuc') as HTMLSelectElement;
    const ngayDang = document.getElementById('ngayDang') as HTMLInputElement;

    if (!tenTruyen.value || !tenTacGia.value || danhMuc.value === '' || !ngayDang.value) {
      return false; // Trả về false nếu có trường nào đó chưa nhập
    }

    return true; // Trả về true nếu tất cả các trường đã được nhập
  }

  validateAndSave() {
    // Kiểm tra các trường dữ liệu ở tab hiện tại
    if (!this.validateCurrentTab()) {
      alert('Vui lòng điền đầy đủ thông tin trước khi đăng!');
      return;
    }

    // Nếu thông tin hợp lệ, thì thực hiện lưu
    this.saveData();
  }

  saveData() {
    // Thực hiện lưu dữ liệu
    console.log('Dữ liệu đã được lưu!');
  }
}
