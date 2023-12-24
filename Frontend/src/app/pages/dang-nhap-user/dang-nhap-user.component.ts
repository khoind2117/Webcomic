import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-dang-nhap-user',
  templateUrl: './dang-nhap-user.component.html',
  styleUrls: ['./dang-nhap-user.component.css']
})
export class DangNhapUserComponent {

  constructor(private authService: AuthService) { }
  
  loginData = {
    UserName: '',
    Password: ''
  };

  login() {
    this.authService.login(this.loginData).subscribe(
      (res) => {
        // Xử lý phản hồi khi đăng nhập thành công
        console.log('Login successful', res);
        // Lưu token vào local storage hoặc xử lý tiếp
      },
      (error) => {
        // Xử lý lỗi khi đăng nhập không thành công
        console.error('Login failed', error);
      }
    );
  }
}
