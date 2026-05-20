ECommerce Platform

Modern e-ticaret süreçlerini simüle eden, ASP.NET Core .NET 8 kullanılarak geliştirilmiş katmanlı mimariye sahip bir e-ticaret platformudur.

 Kullanılan Teknolojiler
 
ASP.NET Core .NET 8
ASP.NET MVC
Entity Framework Core
MSSQL Server
JWT Authentication
Bootstrap 5
Layered Architecture


Özellikler

Kullanıcı İşlemleri
Kullanıcı kayıt olma
Kullanıcı giriş / çıkış işlemleri
JWT tabanlı authentication sistemi
Ürün İşlemleri
Ürün listeleme
Ürün arama ve filtreleme
Kampanyalı ürün gösterimi
Ürün detay kartları
Sepet ve Sipariş
Sepete ürün ekleme / silme
Ürün miktarı artırma / azaltma
Sipariş oluşturma
Sipariş geçmişi görüntüleme
Ödeme ve Kargo
Ödeme yöntemi seçimi
Kargo firması seçimi
Sipariş durum takibi
Kargo durumu yönetimi
Admin Panel
Ürün ekleme / silme / güncelleme
Stok yönetimi
Sipariş yönetimi
Sipariş durum güncelleme
Kampanya yönetimi
İndirim kampanyası oluşturma

Kampanya Sistemi

Aktif kampanyalar ürün fiyatlarına dinamik olarak uygulanmaktadır.
Gerçek ürün fiyatı veritabanında değiştirilmeden indirimli fiyat UI üzerinde hesaplanmaktadır.

Authentication

Sistem JWT tabanlı authentication yapısı kullanmaktadır.
Admin ve kullanıcı işlemleri role-based authorization mantığı ile ayrılmıştır.


Proje Yapısı

ECommerce.API
ECommerce.Business
ECommerce.DataAccess
ECommerce.Entities
ECommerce.WebUI
⚙️ Kurulum
Update-Database

Ardından:

Ctrl + F5

ile proje çalıştırılabilir.

## 📝 Not

Kampanya yönetim modülü CRUD işlemleriyle birlikte geliştirilmiştir.
Süre kısıtı sebebiyle kampanya aktif/pasif geçiş sistemi tamamen tamamlanamamış olup mevcut çalışan haliyle projeye dahil edilmiştir.
