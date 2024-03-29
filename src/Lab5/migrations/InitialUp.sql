create table accounts
(
    account_id      bigint primary key generated by default as identity,
    account_balance bigint   not null,
    pin_code        integer not null
);

create table system_passwords
(
    password text primary key
);

create table account_transaction_histories
(
    account_id                 bigint not null references accounts (account_id),
    transaction_number         bigint not null,
    balance_before_transaction bigint not null,
    balance_after_transaction  bigint not null,
    primary key (account_id, transaction_number)
);