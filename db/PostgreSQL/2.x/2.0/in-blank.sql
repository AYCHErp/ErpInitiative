-->-->-- src/Frapid.Web/Areas/InitiativeNepal/db/PostgreSQL/2.x/2.0/src/03.menus/menus.sql --<--<--
DELETE FROM auth.menu_access_policy
WHERE menu_id IN
(
 SELECT menu_id FROM core.menus
 WHERE app_name = 'InitiativeNepal'
);

DELETE FROM auth.group_menu_access_policy
WHERE menu_id IN
(
 SELECT menu_id FROM core.menus
 WHERE app_name = 'InitiativeNepal'
);

DELETE FROM core.menus
WHERE app_name = 'InitiativeNepal';


SELECT * FROM core.create_app('InitiativeNepal', 'InitiativeNepal', 'Initiative Nepal SMS', '1.0', 'MixERP Inc.', 'December 1, 2015', 'blue comments', '/dashboard/in', NULL);

SELECT * FROM core.create_menu('InitiativeNepal', 'Tasks', 'Tasks', '', 'lightning', '');
SELECT * FROM core.create_menu('InitiativeNepal', 'InitiativeNepalSetup', 'InitiativeNepal Setup', '/dashboard/in', 'configure', 'Tasks');


SELECT * FROM auth.create_app_menu_policy
(
	'Admin', 
	core.get_office_id_by_office_name('Default'), 
	'InitiativeNepal',
	'{*}'
);

