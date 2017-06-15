namespace Movie.Migrations
{
    using Movie.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Movie.Models.MovieContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Movie.Models.MovieContext";
        }

        protected override void Seed(Movie.Models.MovieContext context)
        {
            context.Users.AddOrUpdate(i => i.UserId,
                new User
                {
                    UserId = 1,
                    NickName = "test1",
                    Password = "123456",
                    Email = "test1@qq.com",
                    Privilege = 0
                },
                new User
                {
                    UserId = 2,
                    NickName = "test2",
                    Password = "123456",
                    Email = "test2@qq.com",
                    Privilege = 1
                },
                new User
                {
                    UserId = 3,
                    NickName = "test3",
                    Password = "123456",
                    Email = "test3@qq.com",
                    Privilege = 2
                },
                new User
                {
                    UserId = 4,
                    NickName = "test4",
                    Password = "123456",
                    Email = "test4@qq.com",
                    Privilege = 3
                }
            );
            context.Videos.AddOrUpdate(i => i.VideoId,
                new Video
                {
                    VideoId = 1,
                    Vname = "肖申克的救赎",
                    Vurl = "~/Video/1.mp4",
                    Thumbnail = "~/Image/1.jpg",
                    ViewedNum = 0,
                    UploadTime = DateTime.Now.ToString(),
                    Vtype = "犯罪",
                    UserId = 3,
                    Vinfo = "20世纪40年代末，小有成就的青年银行家安迪（蒂姆·罗宾斯 Tim Robbins 饰）因涉嫌杀害妻子及她的情人而锒铛入狱。在这座名为肖申克的监狱内，希望似乎虚无缥缈，终身监禁的惩罚无疑注定了安迪接下来灰暗绝望的人生。未过多久，安迪尝试接近囚犯中颇有声望的瑞德（摩根·弗里曼 Morgan Freeman 饰），请求对方帮自己搞来小锤子。以此为契机，二人逐渐熟稔，安迪也仿佛在鱼龙混杂、罪恶横生、黑白混淆的牢狱中找到属于自己的求生之道。他利用自身的专业知识，帮助监狱管理层逃税、洗黑钱，同时凭借与瑞德的交往在犯人中间也渐渐受到礼遇。表面看来，他已如瑞德那样对那堵高墙从憎恨转变为处之泰然，但是对自由的渴望仍促使他朝着心中的希望和目标前进。而关于其罪行的真相，似乎更使这一切朝前推进了一步……",

                },
                new Video
                {
                    VideoId = 2,
                    Vname = "这个杀手不太冷",
                    Vurl = "~/Video/1.mp4",
                    Thumbnail = "~/Image/2.jpg",
                    ViewedNum = 0,
                    UploadTime = DateTime.Now.ToString(),
                    Vtype = "动作",
                    UserId = 3,
                    Vinfo = "里昂（让·雷诺饰）是名孤独的职业杀手，受人雇佣。一天，邻居家小姑娘马蒂尔达（纳塔丽·波特曼饰)敲开他的房门，要求在他那里暂避杀身之祸。原来邻居家的主人是警方缉毒组的眼线，只因贪污了一小包毒品而遭恶警（加里·奥德曼饰）杀害全家的惩罚。马蒂尔达得到里昂的留救，幸免于难，并留在里昂那里。里昂教小女孩使枪，她教里昂法文，两人关系日趋亲密，相处融洽。 女孩想着去报仇，反倒被抓，里昂及时赶到，将女孩救回。混杂着哀怨情仇的正邪之战渐次升级，更大的冲突在所难免……",

                },
                new Video
                {
                    VideoId = 3,
                    Vname = "霸王别姬",
                    Vurl = "~/Video/1.mp4",
                    Thumbnail = "~/Image/3.jpg",
                    ViewedNum = 0,
                    UploadTime = DateTime.Now.ToString(),
                    Vtype = "剧情",
                    UserId = 3,
                    Vinfo = "段小楼（张丰毅）与程蝶衣（张国荣）是一对打小一起长大的师兄弟，两人一个演生，一个饰旦，一向配合天衣无缝，尤其一出《霸王别姬》，更是誉满京城，为此，两人约定合演一辈子《霸王别姬》。但两人对戏剧与人生关系的理解有本质不同，段小楼深知戏非人生，程蝶衣则是人戏不分。 段小楼在认为该成家立业之时迎娶了名妓菊仙（巩俐），致使程蝶衣认定菊仙是可耻的第三者，使段小楼做了叛徒，自此，三人围绕一出《霸王别姬》生出的爱恨情仇战开始随着时代风云的变迁不断升级，终酿成悲剧",

                },
                new Video
                {
                    VideoId = 4,
                    Vname = "阿甘正传",
                    Vurl = "~/Video/1.mp4",
                    Thumbnail = "~/Image/4.jpg",
                    ViewedNum = 0,
                    UploadTime = DateTime.Now.ToString(),
                    Vtype = "剧情",
                    UserId = 3,
                    Vinfo = "阿甘（汤姆·汉克斯 饰）于二战结束后不久出生在美国南方阿拉巴马州一个闭塞的小镇，他先天弱智，智商只有75，然而他的妈妈是一个性格坚强的女性，她常常鼓励阿甘“傻人有傻福”，要他自强不息。 阿甘像普通孩子一样上学，并且认识了一生的朋友和至爱珍妮（罗宾·莱特·潘 饰），在珍妮和妈妈的爱护下，阿甘凭着上帝赐予的“飞毛腿”开始了一生不停的奔跑。 阿甘成为橄榄球巨星、越战英雄、乒乓球外交使者、亿万富翁，但是，他始终忘不了珍妮，几次匆匆的相聚和离别，更是加深了阿甘的思念。 有一天，阿甘收到珍妮的信，他们终于又要见面……",

                },
                new Video
                {
                    VideoId = 5,
                    Vname = "美丽人生",
                    Vurl = "~/Video/1.mp4",
                    Thumbnail = "~/Image/5.jpg",
                    ViewedNum = 0,
                    UploadTime = DateTime.Now.ToString(),
                    Vtype = "战争",
                    UserId = 3,
                    Vinfo = "犹太青年圭多（罗伯托·贝尼尼）邂逅美丽的女教师多拉（尼可莱塔·布拉斯基），他彬彬有礼的向多拉鞠躬：“早安！公主！”。历经诸多令人啼笑皆非的周折后，天遂人愿，两人幸福美满的生活在一起。 然而好景不长，法西斯政权下，圭多和儿子被强行送往犹太人集中营。多拉虽没有犹太血统，毅然同行，与丈夫儿子分开关押在一个集中营里。聪明乐天的圭多哄骗儿子这只是一场游戏，奖品就是一辆大坦克，儿子快乐、天真的生活在纳粹的阴霾之中。尽管集中营的生活艰苦寂寞，圭多仍然带给他人很多快乐，他还趁机在纳粹的广播里问候妻子：“早安！公主！” 法西斯政权即将倾覆，纳粹的集中营很快就要接受最后的清理，圭多编给儿子的游戏该怎么结束？他们一家能否平安的度过这黑暗的年代呢？",

                },
                new Video
                {
                    VideoId = 6,
                    Vname = "千与千寻",
                    Vurl = "~/Video/1.mp4",
                    Thumbnail = "~/Image/6.jpg",
                    ViewedNum = 0,
                    UploadTime = DateTime.Now.ToString(),
                    Vtype = "动画",
                    UserId = 3,
                    Vinfo = "千寻和爸爸妈妈一同驱车前往新家，在郊外的小路上不慎进入了神秘的隧道——他们去到了另外一个诡异世界—一个中世纪的小镇。远处飘来食物的香味，爸爸妈妈大快朵颐，孰料之后变成了猪！这时小镇上渐渐来了许多样子古怪、半透明的人。 千寻仓皇逃出，一个叫小白的人救了他，喂了她阻止身体消 失的药，并且告诉她怎样去找锅炉爷爷以及汤婆婆，而且必须获得一分工作才能不被魔法变成别的东西。 千寻在小白的帮助下幸运地获得了一份在浴池打杂的工作。渐渐她不再被那些怪模怪样的人吓倒，并从小玲那儿知道了小白是凶恶的汤婆婆的弟子。 一次，千寻发现小白被一群白色飞舞的纸人打伤，为了救受伤的小白，她用河神送给她的药丸驱出了小白身体内的封印以及守封印的小妖精，但小白还是没有醒过来。 为了救小白，千寻又踏上了她的冒险之旅。",

                },
                new Video
                {
                    VideoId = 7,
                    Vname = "辛德勒的名单",
                    Vurl = "~/Video/1.mp4",
                    Thumbnail = "~/Image/7.jpg",
                    ViewedNum = 0,
                    UploadTime = DateTime.Now.ToString(),
                    Vtype = "战争",
                    UserId = 3,
                    Vinfo = "1939年，波兰在纳粹德国的统治下，党卫军对犹太人进行了隔离统治。德国商人奥斯卡·辛德勒（连姆·尼森 Liam Neeson 饰）来到德军统治下的克拉科夫，开设了一间搪瓷厂，生产军需用品。凭着出众的社交能力和大量的金钱，辛德勒和德军建立了良好的关系，他的工厂雇用犹太人工作，大发战争财。 1943年，克拉科夫的犹太人遭到了惨绝人寰的大屠杀，辛德勒目睹这一切，受到了极大的震撼，他贿赂军官，让自己的工厂成为集中营的附属劳役营，在那些疯狂屠杀的日子里，他的工厂也成为了犹太人的避难所。 1944年，德国战败前夕，屠杀犹太人的行动越发疯狂，辛德勒向德军军官开出了1200人的名单，倾家荡产买下了这些犹太人的生命。在那些暗无天日的岁月里，拯救一个人，就是拯救全世界。",

                },
                new Video
                {
                    VideoId = 8,
                    Vname = "泰坦尼克号",
                    Vurl = "~/Video/1.mp4",
                    Thumbnail = "~/Image/8.jpg",
                    ViewedNum = 0,
                    UploadTime = DateTime.Now.ToString(),
                    Vtype = "爱情",
                    UserId = 3,
                    Vinfo = "1912年4月10日，号称 “世界工业史上的奇迹”的豪华客轮泰坦尼克号开始了自己的处女航，从英国的南安普顿出发驶往美国纽约。富家少女罗丝（凯特•温丝莱特）与母亲及未婚夫卡尔坐上了头等舱；另一边，放荡不羁的少年画家杰克（莱昂纳多·迪卡普里奥）也在码头的一场赌博中赢得了下等舱的船票。 罗丝厌倦了上流社会虚伪的生活，不愿嫁给卡尔，打算投海自尽，被杰克救起。很快，美丽活泼的罗丝与英俊开朗的杰克相爱，杰克带罗丝参加下等舱的舞会、为她画像，二人的感情逐渐升温。 1912年4月14日，星期天晚上，一个风平浪静的夜晚。泰坦尼克号撞上了冰山，“永不沉没的”泰坦尼克号面临沉船的命运，罗丝和杰克刚萌芽的爱情也将经历生死的考验。",

                },
                new Video
                {
                    VideoId = 9,
                    Vname = "盗梦空间",
                    Vurl = "~/Video/1.mp4",
                    Thumbnail = "~/Image/9.jpg",
                    ViewedNum = 0,
                    UploadTime = DateTime.Now.ToString(),
                    Vtype = "悬疑",
                    UserId = 3,
                    Vinfo = "道姆·柯布（莱昂纳多·迪卡普里奥 Leonardo DiCaprio 饰）与同事阿瑟（约瑟夫·戈登-莱维特 Joseph Gordon-Levitt 饰）和纳什（卢卡斯·哈斯 Lukas Haas 饰）在一次针对日本能源大亨齐藤（渡边谦 饰）的盗梦行动中失败，反被齐藤利用。齐藤威逼利诱因遭通缉而流亡海外的柯布帮他拆分他竞争对手的公司，采取极端措施在其唯一继承人罗伯特·费希尔（希里安·墨菲 Cillian Murphy 饰）的深层潜意识中种下放弃家族公司、自立门户的想法。为了重返美国，柯布偷偷求助于岳父迈尔斯（迈克尔·凯恩 Michael Caine 饰），吸收了年轻的梦境设计师艾里阿德妮（艾伦·佩吉 Ellen Page 饰）、梦境演员艾姆斯（汤姆·哈迪 Tom Hardy 饰）和药剂师约瑟夫（迪利普·劳 Dileep Rao 饰）加入行动。在一层层递进的梦境中，柯布不仅要对付费希尔潜意识的本能反抗，还必须直面已逝妻子梅尔（玛丽昂·歌迪亚 Marion Cotillard 饰）的处处破坏，实际情况远比预想危险得多……",

                },
                new Video
                {
                    VideoId = 10,
                    Vname = "机器人总动员",
                    Vurl = "~/Video/1.mp4",
                    Thumbnail = "~/Image/10.jpg",
                    ViewedNum = 0,
                    UploadTime = DateTime.Now.ToString(),
                    Vtype = "动画",
                    UserId = 3,
                    Vinfo = "公元2805年，人类文明高度发展，却因污染和生活垃圾大量增加使得地球不再适于人类居住。地球人被迫乘坐飞船离开故乡，进行一次漫长无边的宇宙之旅。临行前他们委托Buynlarge的公司对地球垃圾进行清理，该公司开发了名为WALL·E（Waste Allocation Loa d Lifters – Earth 地球废品分装员）的机器人担当此重任。 这些机器人按照程序日复一日、年复一年辛勤工作，但随着时间的流逝和恶劣环境的侵蚀，WALL·E们接连损坏、停止运动。最后只有一个仍在进行这项似乎永无止境的工作。经历了漫长的岁月，它开始拥有了自己的意识。它喜欢将收集来的宝贝小心翼翼藏起，喜欢收工后看看几百年前的歌舞片，此外还有一只蟑螂朋友作伴。直到有一天，一艘来自宇宙的飞船打破了它一成不变的生活…… 本片荣获2009年第81届奥斯卡最佳动画长片奖。",

                },
                new Video
                {
                    VideoId = 11,
                    Vname = "海上钢琴师",
                    Vurl = "~/Video/1.mp4",
                    Thumbnail = "~/Image/11.jpg",
                    ViewedNum = 0,
                    UploadTime = DateTime.Now.ToString(),
                    Vtype = "音乐",
                    UserId = 3,
                    Vinfo = "本片讲述了一个钢琴天才传奇的一生。 1900年，Virginian号豪华邮轮上，一个孤儿被遗弃在头等舱，由船上的水手抚养长大，取名1900（蒂姆•罗斯 饰）。1900慢慢长大，显示出了无师自通的非凡钢琴天赋，在船上的乐队表演钢琴，每个听过他演奏的人，都被深深打动。爵士 乐鼻祖杰尼听说了1900的高超技艺，专门上船和他比赛，最后自叹弗如，黯然离去。 可惜，这一切的事情都发生在海上，1900从来不愿踏上陆地，直到有一天，他爱上了一个女孩，情愫在琴键上流淌。他会不会为了爱情，踏上陆地开始新的生活，用他的琴声惊艳世界？他将怎样谱写自己非凡的人生。",

                },
                new Video
                {
                    VideoId = 12,
                    Vname = "三傻大闹宝莱坞",
                    Vurl = "~/Video/1.mp4",
                    Thumbnail = "~/Image/12.jpg",
                    ViewedNum = 0,
                    UploadTime = DateTime.Now.ToString(),
                    Vtype = "喜剧",
                    UserId = 3,
                    Vinfo = "本片根据印度畅销书作家奇坦·巴哈特（Chetan Bhagat）的处女作小说《五点人》（Five Point Someone）改编而成。法兰（马德哈万 R Madhavan 饰）、拉杜（沙曼·乔希 Sharman Joshi 饰）与兰乔（阿米尔·汗 Aamir Khan 饰）是皇家工程学院的学生，三人共居一室，结为好友。在以严格著称的学院里，兰乔是个非常与众不同的学生，他不死记硬背，甚至还公然顶撞校长“病毒”（波曼·伊拉尼 Boman Irani 饰），质疑他的教学方法。他不仅鼓动法兰与拉杜去勇敢追寻理想，还劝说校长的二女儿碧雅（卡琳娜·卡普 Kareena Kapoor 饰）离开满眼铜臭的未婚夫。兰乔的特立独行引起了模范学生“消音器”（奥米·维嘉 Omi Vaidya 饰）的不满，他约定十年后再与兰乔一决高下，看哪种生活方式更能取得成功。 本片获孟买电影博览奖最佳影片、最佳导演、最佳配角（波曼·伊拉尼）、最佳剧本等六项大奖，并获国际印度电影协会最佳影片、最佳导演、最佳剧情、最佳摄影等十六项大奖",

                },
                new Video
                {
                    VideoId = 13,
                    Vname = "忠犬八公的故事",
                    Vurl = "~/Video/1.mp4",
                    Thumbnail = "~/Image/13.jpg",
                    ViewedNum = 0,
                    UploadTime = DateTime.Now.ToString(),
                    Vtype = "剧情",
                    UserId = 3,
                    Vinfo = "八公（Forest 饰）是一条谜一样的犬，因为没有人知道它从哪里来。教授帕克（理查·基尔 Richard Gere 饰）在小镇的火车站拣到一只走失的小狗，冥冥中似乎注定小狗和帕克教授有着某种缘分，帕克一抱起这只小狗就再也放不下来，最终，帕克对小狗八公的疼爱感化了起初极力反对养狗的妻子卡特（琼·艾伦 Joan Allen 饰）。八公在帕克的呵护下慢慢长大，帕克上班时八公会一直把他送到车站，下班时八公也会早早便爬在车站等候，八公的忠诚让小镇的人家对它更加疼爱。有一天，八公在帕克要上班时表现异常，居然玩起了以往从来不会的捡球游戏，八公的表现让帕克非常满意，可是就是在那天，帕克因病去世。帕克的妻子、女儿安迪（萨拉·罗默尔 Sarah Roemer 饰）及女婿迈克尔（罗比·萨布莱特 Robbie Sublett 饰）怀着无比沉痛的心情埋葬了帕克，可是不明就里的八公却依然每天傍晚五点准时守候在小站的门前，等待着主人归来…… 本片根据上个世纪30年代发生在日本的真实故事改编，1987年在日本拍成电影后创造了40亿日元的票房。",

                },
                new Video
                {
                    VideoId = 14,
                    Vname = "放牛班的春天",
                    Vurl = "~/Video/1.mp4",
                    Thumbnail = "~/Image/14.jpg",
                    ViewedNum = 0,
                    UploadTime = DateTime.Now.ToString(),
                    Vtype = "音乐",
                    UserId = 3,
                    Vinfo = "1949年的法国乡村，音乐家克莱门特（杰勒德•尊诺 饰）到了一间外号叫“塘低”的男子寄宿学校当助理教师。学校里的学生大部分都是难缠的问题儿童，体罚在这里司空见惯，学校的校长（弗朗西斯•贝尔兰德 饰）只顾自己的前途，残暴高压。 性格沉静的克莱门特尝试用自己的方法改善这种状况，他重新创作音乐作品，组织合唱团，决定用音乐的方法来打开学生们封闭的心灵。 然而，事情并不顺利，克莱门特发现学生皮埃尔•莫安琦（尚•巴堤•莫里耶 饰）拥有非同一般的音乐天赋，但是单亲家庭长大的他，性格异常敏感孤僻，怎样释放皮埃尔的音乐才能，让克莱门特头痛不已；同时，他与皮埃尔母亲的感情也渐渐微妙起来。",

                },
                new Video
                {
                    VideoId = 15,
                    Vname = "教父",
                    Vurl = "~/Video/1.mp4",
                    Thumbnail = "~/Image/15.jpg",
                    ViewedNum = 0,
                    UploadTime = DateTime.Now.ToString(),
                    Vtype = "犯罪",
                    UserId = 3,
                    Vinfo = "40年代的美国，“教父”维托·唐·柯里昂（马龙·白兰度 饰）是黑手党柯里昂家族的首领，带领家族从事非法的勾当，但同时他也是许多弱小平民的保护神，深得人们爱戴。 因为拒绝了毒枭索洛索的毒品交易要求，柯里昂家族和纽约其他几个黑手党家族的矛盾激化。圣诞前夕，索洛索劫持了“教 父”的参谋汤姆，并派人暗杀“教父”；因为内奸的出卖，“教父”的大儿子逊尼被仇家杀害；小儿子麦克（阿尔·帕西诺 饰）也被卷了进来，失去爱妻。黑手党家族之间的矛盾越来越白热化。 年老的“教父”面对丧子之痛怎样统领全局？黑手党之间的仇杀如何落幕？谁是家族的内奸？谁又能够成为新一代的“教父”？ 血雨腥风和温情脉脉，在这部里程碑式的黑帮史诗巨片里真实上演。",

                }
            );
        }
    }
}
