# SimofunCase

DependencyInversion: 

"AnswerToDependencyInversionCase" bu dosya da sorunun çözülmüş hali var ve aşağıda ise ne tür çözümler uyguladığımı ayrıntılı bir şekilde anlattım.

Sorunuza göre, LeaderboardController sınıfının LeaderboardSorterByScore ve LeaderboardSorterByName sınıflarına olan doğrudan bağımlılığını kaldırmak istiyorsunuz. Bunun için Dependency Inversion prensibini uygulayabiliriz. Bu prensibe göre, yüksek seviye modüller düşük seviye modüllere doğrudan bağlı olmamalıdır. Bu durumda, LeaderboardController sınıfı yüksek seviye bir modül iken LeaderboardSorterByScore ve LeaderboardSorterByName sınıfları düşük seviye modüllerdir.

Bunu çözmek için genellikle bir arayüz (interface) tanımlanır ve bu arayüzü LeaderboardController sınıfı tarafından kullanırız. Daha sonra, LeaderboardSorterByScore ve LeaderboardSorterByName sınıfları bu arayüzü uygular ve LeaderboardController sınıfına bu arayüzü kullanarak erişiriz. Bu şekilde LeaderboardController sınıfı, hangi sıralama stratejisinin kullanılacağını bilmek zorunda kalmaz ve bu sınıfların değişmesi veya başka bir sınıfın eklenmesi durumunda kodda minimum değişiklik yapılması gerekir.

Verdiğiniz LeaderboardSorterByName sınıfı LeaderboardSorterByScore sınıfları ILeaderboardSorter interfacesini implement eder.

LeaderboardController sınıfını da ILeaderboardSorter arayüzünü kullanacak şekilde düzenledim

ILeaderboardProvider arayüzünü oluşturup FakeLeaderboardProvider sınıfındaki bağımlılığıkları ortadan kaldırmış oluyoruz bu interfaceyi implement ediyoruz.(Interface Segregation prensibi.)

farklı sorter türlerimizi oluşturmak için ILeaderboardSorterFactory  arayüzünü ve LeaderboardSorterFactory  sınıfını oluşturuyorum.(factory design pattern.)

Gözlemci deseni, bir nesnenin durumu değiştiğinde diğer nesnelere bu değişikliklerin bildirilmesini sağlar. Bu örnekte, sıralama türü değiştiğinde diğer bileşenlere bildirim gönderebiliriz. bunun için de LeaderboardSortTypeSubject sınıfını ve ILeaderboardSortTypeObserver interfacesini oluşturdum (Observer Design Pattern)

Mekanikler:

"Mecahnics" dosyasında bir unity projesi var ve istenen üç oyun mekaniği tek bir projede yapılmıştır.
"MechanicsBuilds" dosyasında hızlı deneme için mekaniklerin teker teker build alınmış halleri var.


 
