// chinh-sua-truyen.component.ts

import { Component, OnInit, Renderer2, ElementRef } from '@angular/core';
import { AngularEditorConfig } from '@kolkov/angular-editor';
@Component({
  selector: 'app-chinh-sua-truyen',
  templateUrl: './chinh-sua-truyen.component.html',
  styleUrls: ['./chinh-sua-truyen.component.css']
})
export class ChinhSuaTruyenComponent implements OnInit {

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
}
