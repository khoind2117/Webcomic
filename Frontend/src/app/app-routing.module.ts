import { NgModule } from '@angular/core';
import { RouterModule, Routes, mapToCanActivate } from '@angular/router';
import { InfoTruyenComponent } from './pages/info-truyen/info-truyen.component'; // Import InfoTruyenComponent
import { HomeComponent } from './pages/home/home.component';
import { DanhmucComponent } from './pages/danhmuc/danhmuc.component';
import { DMPhieuLuuComponent } from './pages/dmphieu-luu/dmphieu-luu.component';
import { ChinhSachComponent } from './pages/chinh-sach/chinh-sach.component';
import { DangNhapComponent } from './pages/dang-nhap/dang-nhap.component';
import { DocTruyenComponent } from './pages/doc-truyen/doc-truyen.component';
import { DangKiComponent } from './pages/dang-ki/dang-ki.component';
import { ThongTinUserComponent } from './pages/thong-tin-user/thong-tin-user.component';
import { DangTruyenComponent } from './pages/dang-truyen/dang-truyen.component';
import { ChinhSuaTruyenComponent } from './pages/chinh-sua-truyen/chinh-sua-truyen.component';
import { ThemTruyenMoiComponent } from './pages/them-truyen-moi/them-truyen-moi.component';
import { TruyenTheoTagComponent } from './pages/truyen-theo-tag/truyen-theo-tag.component';
import { DangNhapUserComponent } from './pages/dang-nhap-user/dang-nhap-user.component';
import { DangKiUserComponent } from './pages/dang-ki-user/dang-ki-user.component';


const routes: Routes = [
  { path: 'the-loai/:id/:page', component: TruyenTheoTagComponent },
  { path: 'dang-ki-user', component: DangKiUserComponent},
  { path: 'dang-nhap-user', component: DangNhapUserComponent},

  {
    path: 'info-truyen',
    component: InfoTruyenComponent
  }, // Sử dụng InfoTruyenComponent
  {
    path: '',
    component: HomeComponent,
    pathMatch: 'full'
  },
  {
    path: 'DanhMuc',
    component: DanhmucComponent
  },
  {
    path: 'DanhMucPhieuLuu',
    component: DMPhieuLuuComponent
  },
  {
    path: 'DangTruyen',
    component: DangTruyenComponent
  },
  {
    path: 'ChinhSach',
    component: ChinhSachComponent
  },
  {
    path: 'DangNhap',
    component: DangNhapComponent
  },
  {
    path: 'DocTruyen',
    component: DocTruyenComponent
  },
  {
    path: 'DangKi',
    component: DangKiComponent
  },
  {
    path: 'ThongTinUser',
    component: ThongTinUserComponent
  },
  {
    path: 'ChinhSuaTruyen',
    component: ChinhSuaTruyenComponent
  },
  {
    path: 'ThemTruyenMoi',
    component: ThemTruyenMoiComponent
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
