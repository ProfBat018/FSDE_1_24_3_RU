# BankTask Mikroservis Layihəsi

Bu layihə bank sistemləri üçün hazırlanmış mikroservis arxitekturası üzərində qurulmuşdur. Layihə aşağıdakı əsas funksional komponentlərə malikdir:

## Texnologiyalar

- ASP.NET Core 9.0
- RabbitMQ
- MongoDB
- MinIO
- MSSQL Server
- Kubernetes (Minikube)
- gRPC
- AutoMapper
- Docker, DockerHub
- Scalar (Swagger əvəzinə)
- Ocelot API Gateway

## Mikroservislər

### 1. **UserService**
İstifadəçilərin yaradılması, yenilənməsi və silinməsi funksionallığını təmin edir. 
- gRPC vasitəsilə digər servislərə istifadəçi məlumatlarını təqdim edir.
- RabbitMQ ilə `user.created`, `user.updated`, `user.deleted` hadisələrini yayır.

### 2. **ContactService**
İstifadəçilərin əlaqə məlumatlarını (email, telefon) saxlayır və doğrulama imkanı verir.
- `GrpcUserClient` ilə `UserService` üzərindən `UserId` alır.
- `contact.added` hadisəsini RabbitMQ ilə paylaşır.

### 3. **ImageService**
MinIO üzərindən şəkil yükləmə və saxlanmasını təmin edir.
- Fayl metadata-sı MSSQL-də saxlanılır.

### 4. **DocumentService**
Kredit üçün sənədlərin yüklənməsi və idarə edilməsi üçün istifadə olunur.
- MinIO-da saxlanır, metadata MSSQL-də qeyd olunur.
- `document.uploaded` hadisəsi RabbitMQ vasitəsilə ötürülür.

### 5. **TransactionService**
İstifadəçilər arasında maliyyə əməliyyatlarını idarə edir.
- `transaction.created` hadisəsini RabbitMQ ilə yayımlayır.

### 6. **NotificationService**
RabbitMQ üzərindən `transaction.created` hadisəsini dinləyir və MongoDB-də bildirişləri saxlayır.

### 7. **AuditService**
RabbitMQ-da yayımlanan bütün hadisələri (`#`) dinləyir və MongoDB-də log kimi saxlayır.

### 8. **ApiGateway**
Ocelot əsasında qurulub və bütün mikroservislərə HTTP üzərindən mərkəzləşdirilmiş çıxış imkanı verir.

## Deployment

Layihə Minikube üzərində yerləşdirilmişdir. Hər servis öz Docker imicinə sahibdir və `DockerHub` üzərindən pull olunur.

### Port Forward Komandaları (test üçün):
```
kubectl port-forward deployment/userservice 5000:80
kubectl port-forward deployment/contactservice 5001:80
kubectl port-forward deployment/imageservice 5002:80
kubectl port-forward deployment/documentservice 5003:80
kubectl port-forward deployment/transactionservice 5004:80
kubectl port-forward deployment/notificationservice 5005:80
kubectl port-forward deployment/auditservice 5006:80
kubectl port-forward deployment/apigateway 8080:80

kubectl port-forward service/rabbitmq 15672:15672
kubectl port-forward service/mongo-express 8081:8081
kubectl port-forward service/minio 9001:9001
```

## MinIO Giriş
MinIO-u seçməyimin səbəbi odur ki, lokal olaraq S3 dəstəyi təmin edən yüngül və rahat obyekt saxlama sistemidir. Şəkillər və sənədlər üçün ideal bir həlldir.
- URL: http://localhost:9001
- Login: minioadmin
- Parol: minioadmin

## RabbitMQ Giriş
- URL: http://localhost:15672
- Login: guest
- Parol: guest

## Mongo Express Giriş
- URL: http://localhost:8081
- Login: admin
- Parol: admin123

## Scalar (Swagger əvəzinə)
Layihədə Scalar istifadə olunub, çünki Swagger son versiyalarda .NET 9 ilə Swachbukle.AspNetCore kitabxanası artıq default olaraq dəstəklənmir . Scalar daha yüngül və sadə alternativdir.

## Migration Təlimatı (EF Core)

Hər mikroservisin öz `DbContext` və `DbContextFactory`-si vardır. Mühit hazır olduqdan sonra aşağıdakı adımlarla migrasiyalar tətbiq olunur:

1. Servis qovluğuna daxil olun:
```bash
cd UserService.API
```

2. Yeni migration yaradın:
```bash
dotnet ef migrations add Initial --project ../UserService.Data --startup-project .
```

3. Verilənlər bazasına tətbiq edin:
```bash
dotnet ef database update --project ../UserService.Data --startup-project .
```

Bu addımlar digər servislər üçün də eyni qaydada istifadə olunur (`ContactService`, `ImageService`, s.).

## Quraşdırma və Test
1. `appsettings.json` faylı uyğun şəkildə konfiqurasiya edilməlidir. Hər ehtimala qarşı saxlayacam
2. Minikube cluster aktiv edilməlidir.
3. `kubectl apply -f k8s/` ilə deploymentlər tətbiq olunur.
4. Port forward edilərək servis testləri həyata keçirilir.

## Müəllif
Bu layihə bank sistemləri üçün real mikroservis nümunəsi olaraq hazırlanmışdır və intervü məqsədilə təqdim olunur.