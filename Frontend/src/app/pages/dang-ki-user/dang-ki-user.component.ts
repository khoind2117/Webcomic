import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-dang-ki-user',
  templateUrl: './dang-ki-user.component.html',
  styleUrls: ['./dang-ki-user.component.css']
})
export class DangKiUserComponent {
  
  constructor(private authService: AuthService) { }
  
  registerData = {
    UserName: '',
    Email: '',
    Password: '',
    confirmPassword: ''
  };

  register() {
    if (this.registerData.Password !== this.registerData.confirmPassword) {
      // Xử lý khi mật khẩu và xác nhận mật khẩu không khớp
      console.error('Passwords do not match');
      return;
    }

    this.authService.register(this.registerData).subscribe(
      (res) => {
        // Xử lý phản hồi khi đăng ký thành công
        console.log('Registration successful', res);
        // Lưu token vào local storage hoặc xử lý tiếp
      },
      (error) => {
        // Xử lý lỗi khi đăng ký không thành công
        console.error('Registration failed', error);
      }
    );
  }

}
