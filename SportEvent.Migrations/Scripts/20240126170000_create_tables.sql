create schema if not exists dcs_sport_event authorization current_user;

create table if not exists dcs_sport_event.broadcasts
(
	id bigserial primary key,
	home_team varchar(30) not null,
	guest_team varchar(30) not null,
	start_date date not null,
	start_time time not null,
	status integer not null
);

create table if not exists dcs_sport_event.messages
(
	id bigserial primary key,
	time time not null,
	action varchar(30),
	value varchar(30),
	subject varchar(30),
	info varchar(30) not null,
	broadcast_id bigserial not null,
	foreign key (broadcast_id) references dcs_sport_event.broadcasts (id) on delete cascade
);

comment on table dcs_sport_event.broadcasts is 'Таблица трансляций';
comment on table dcs_sport_event.messages is 'Таблица сообщений';

comment on column dcs_sport_event.broadcasts.id is 'Идентификатор трансляции';
comment on column dcs_sport_event.broadcasts.home_team is 'Команда, играющая дома';
comment on column dcs_sport_event.broadcasts.guest_team is 'Команда, играющая в гостях';
comment on column dcs_sport_event.broadcasts.start_date is 'Дата начала трансляции';
comment on column dcs_sport_event.broadcasts.start_time is 'Время начала трансляции';
comment on column dcs_sport_event.broadcasts.status is 'Статус трансляции';

comment on column dcs_sport_event.messages.id is 'Идентификатор сообщения';
comment on column dcs_sport_event.messages.time is 'Время события';
comment on column dcs_sport_event.messages.action is 'Действие';
comment on column dcs_sport_event.messages.value is 'Значение';
comment on column dcs_sport_event.messages.subject is 'Субъект';
comment on column dcs_sport_event.messages.info is 'Основная информация';
comment on column dcs_sport_event.messages.broadcast_id is 'Идентификатор трансляции, к которой относится сообщение';