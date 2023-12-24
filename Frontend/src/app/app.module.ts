import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { InfoTruyenRoutingModule } from './pages/info-truyen/info-truyen-routing.module';
import { InfoTruyenComponent } from './pages/info-truyen/info-truyen.component';
import { HomeComponent } from './pages/home/home.component';
import { DanhmucComponent } from './pages/danhmuc/danhmuc.component';
import { DMPhieuLuuComponent } from './pages/dmphieu-luu/dmphieu-luu.component';
import { ChinhSachComponent } from './pages/chinh-sach/chinh-sach.component';
import { DangNhapComponent } from './pages/dang-nhap/dang-nhap.component';
import { DocTruyenComponent } from './pages/doc-truyen/doc-truyen.component';
import { DangKiComponent } from './pages/dang-ki/dang-ki.component';
import { ThongTinUserComponent } from './pages/thong-tin-user/thong-tin-user.component';
import { DangTruyenComponent } from './pages/dang-truyen/dang-truyen.component';
import { SuaTruyenComponent } from './pages/sua-truyen/sua-truyen.component';
import { ChinhSuaTruyenComponent } from './pages/chinh-sua-truyen/chinh-sua-truyen.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { ThemTruyenMoiComponent } from './pages/them-truyen-moi/them-truyen-moi.component';
import { ComicService } from './services/comic.service';
import { TagService } from './services/tag.service';
import { TagListComponent } from './pages/tag-list/tag-list.component';
import { TruyenTheoTagComponent } from './pages/truyen-theo-tag/truyen-theo-tag.component';
import { DangKiUserComponent } from './pages/dang-ki-user/dang-ki-user.component';
import { DangNhapUserComponent } from './pages/dang-nhap-user/dang-nhap-user.component';
import { AuthService } from './services/auth.service';


@NgModule({
  declarations: [
    AppComponent,

    InfoTruyenComponent,
    HomeComponent,
    DanhmucComponent,
    DMPhieuLuuComponent,
    ChinhSachComponent,
    DangNhapComponent,
    DocTruyenComponent,
    DangKiComponent,
    ThongTinUserComponent,
    DangTruyenComponent,
    SuaTruyenComponent,
    ChinhSuaTruyenComponent,
    ThemTruyenMoiComponent,
    TagListComponent,
    TruyenTheoTagComponent,
    DangKiUserComponent,
    DangNhapUserComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AngularEditorModule,
    FormsModule,
    CommonModule,
    HttpClientModule
  ],
  providers: [
    ComicService,
    TagService,
    AuthService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
