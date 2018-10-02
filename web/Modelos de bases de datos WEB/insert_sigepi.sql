insert into tipo_usuario(id_tipo_usuario,nom_tipo_usuario)
	values(1,"Lider"),
		(2,"miembro"),
		(3,"estudiante");
select * from tipo_usuario;

insert into usuario 
	values(11,"antonio","contra_antorio","url.../dsajk",1),
		(12,"carlos","12345","url.../dsajk",2),
		(13,"carlos2","12345","url.../dsajk",2),
		(14,"brayan","yisus","url.../dsajk",3),
		(15,"heri","giecom","gieco",1),
		(16,"micht","giecom1","url.../dsajk",2),
		(17,"alcalde","giecom2","url.../dsajk",2);
		
select * from usuario;

insert into proyecto
	values(22,"AntonioProyect"),
	(23,"Proyect3"),
	(24,"Proyect4"),
	(25,"Proyect5"),
	(26,"Proyect6"),
	(27,"Proyect7");
	
insert into usuario_has_proyecto
	values(11,22),
		(11,23),
		(12,22),
		(13,22),
		(15,24),
		(15,25),
		(15,26),
		(15,27),
		(12,24),
		(13,25),
		(14,24),
		(16,26),
		(16,27),
		(17,27);
		
		
select * from usuario_has_proyecto;

select * from proyecto;

DESCRIBE grupo_inve_semillero;

insert into grupo_inve_semillero
	VALUES(111,"nom_grupo1","hola singf invtiga1","objetivofhdskjha1","programa1","kjlkjdoanm.jpg",
		"misio_grupo1dfsfsafsfs   1","vison     1","justificasion   1","quienes somos     1",null),
		(112,"nom_grupo2","hola singf invtiga2","objetivofhdskjha2","programa2","kjlkjdoanm.jpg",
		"misio_grupo1dfsfsafsfs   2","vison     2","justificasion   2","quienes somos     2",null),
		(113,"nom_grupo3","hola singf invtiga3","objetivofhdskjha3","programa3","kjlkjdoanm.jpg",
		"misio_grupo1dfsfsafsfs   3","vison     3","justificasion   3","quienes somos     3",null),
		(114,"nom_grupo4","hola singf invtiga4","objetivofhdskjha4","programa4","kjlkjdoanm.jpg",
		"misio_grupo1dfsfsafsfs   4","vison     4","justificasion   4","quienes somos     4",null),
		(115,"nom_grupo5","hola singf invtiga5","objetivofhdskjha5","programa5","kjlkjdoanm.jpg",
		"misio_grupo1dfsfsafsfs   5","vison     5","justificasion   5","quienes somos     5",null),
		(116,"nom_grupo6","hola singf invtiga6","objetivofhdskjha6","programa2","kjlkjdoanm.jpg",
		"misio_grupo1dfsfsafsfs   6","vison     6","justificasion   6","quienes somos     6",null),
		(117,"nom_grupo7","hola singf invtiga7","objetivofhdskjha7","programa3","kjlkjdoanm.jpg",
		"misio_grupo1dfsfsafsfs   7","vison     7","justificasion   7","quienes somos     7",null),
		(118,"nom_grupo8","hola singf invtiga8","objetivofhdskjha8","programa4","kjlkjdoanm.jpg",
		"misio_grupo1dfsfsafsfs   8","vison     8","justificasion   8","quienes somos     8",null),
		(119,"nom_grupo9","hola singf invtiga9","objetivofhdskjha9","programa5","kjlkjdoanm.jpg",
		"misio_grupo1dfsfsafsfs   9","vison     9","justificasion   9","quienes somos     9",null),
		(120,"nom_grupo20","hola singf invtiga20","objetivofhdskjha20","programa1","kjlkjdoanm.jpg",
		"misio_grupo1dfsfsafsfs   20","vison     20","justificasion   20","quienes somos     20",null);

select * from grupo_inve_semillero;
	
insert into integrante_has_grupo_inve_semillero
	values(11,111),
		(11,112),
		(11,113),
		(11,114),
		(15,115),
		(15,116),
		(15,117),
		(15,118),
		(15,119),
		(15,120);